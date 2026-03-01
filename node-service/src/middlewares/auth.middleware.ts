import { Request, Response, NextFunction } from 'express';
import { CognitoJwtVerifier } from 'aws-jwt-verify';
import prisma from '../lib/prisma'; //

const verifier = CognitoJwtVerifier.create({
  userPoolId: process.env.COGNITO_USER_POOL_ID || '',
  tokenUse: 'id',
  clientId: process.env.COGNITO_CLIENT_ID || '',
});

export interface AuthRequest extends Request {
  user?: any;
}

export const requireAuth = async (req: AuthRequest, res: Response, next: NextFunction): Promise<void> => {
  try {
    const authHeader = req.headers.authorization;
    if (!authHeader || !authHeader.startsWith('Bearer ')) {
      res.status(401).json({ message: 'Vui lòng đăng nhập!' });
      return;
    }

    const token = authHeader.split(' ')[1];
    const payload = await verifier.verify(token);

    // Tự động đồng bộ: Tạo mới nếu chưa có, cập nhật nếu đã tồn tại
    const user = await prisma.users.upsert({
      where: { cognito_sub: payload.sub },
      update: { full_name: (payload as any).name },
      create: {
        cognito_sub: payload.sub,
        email: (payload as any).email || '',
        full_name: (payload as any).name || 'User',
        role: 'Candidate' 
      }
    });

    req.user = user; 
    next(); 
  } catch (error) {
    console.error('❌ Lỗi xác thực hoặc Sync DB:', error);
    res.status(403).json({ message: 'Token không hợp lệ!' });
  }
};
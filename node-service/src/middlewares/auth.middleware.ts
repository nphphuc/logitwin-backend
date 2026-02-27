import { Request, Response, NextFunction } from 'express';
import { CognitoJwtVerifier } from 'aws-jwt-verify';

// Khởi tạo máy quét thẻ với thông tin Cognito của bạn
const verifier = CognitoJwtVerifier.create({
  userPoolId: process.env.COGNITO_USER_POOL_ID || '',
  tokenUse: 'access',
  clientId: process.env.COGNITO_CLIENT_ID || '',
});

export interface AuthRequest extends Request {
  user?: any;
}

export const requireAuth = async (req: AuthRequest, res: Response, next: NextFunction): Promise<void> => {
  try {
    const authHeader = req.headers.authorization;
    if (!authHeader || !authHeader.startsWith('Bearer ')) {
      res.status(401).json({ message: 'Không tìm thấy thẻ (Token)! Vui lòng đăng nhập.' });
      return;
    }

    const token = authHeader.split(' ')[1];
    const payload = await verifier.verify(token);

    req.user = payload;
    next(); 
  } catch (error) {
    console.error('Lỗi xác thực Token:', error);
    res.status(403).json({ message: 'Thẻ (Token) không hợp lệ hoặc đã hết hạn!' });
  }
};
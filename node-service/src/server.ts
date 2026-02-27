import 'dotenv/config'; 
import express, { Request, Response } from 'express';
import cors from 'cors';
import { requireAuth, AuthRequest } from './middlewares/auth.middleware';

// Swagger
import swaggerUi from 'swagger-ui-express';
import swaggerJsdoc from 'swagger-jsdoc';

const app = express();
const port = process.env.PORT || 4000;

app.use(cors());
app.use(express.json());

// 1. Cấu hình Swagger bằng Code JSON (Không dùng comment YAML nữa)
const swaggerOptions = {
  definition: {
    openapi: '3.0.0',
    info: {
      title: 'SmartHire Node.js API',
      version: '1.0.0',
      description: 'Tài liệu API cho Microservice Node.js',
    },
    components: {
      securitySchemes: {
        bearerAuth: {
          type: 'http',
          scheme: 'bearer',
          bearerFormat: 'JWT',
        },
      },
    },
    paths: {
      '/api/health': {
        get: {
          summary: 'Kiểm tra trạng thái Server',
          responses: {
            '200': { description: 'Trả về trạng thái hoạt động của server' }
          }
        }
      },
      '/api/protected': {
        get: {
          summary: 'API cần có Token để gọi',
          security: [{ bearerAuth: [] }],
          responses: {
            '200': { description: 'Trả về thông tin giải mã từ Token' },
            '401': { description: 'Không tìm thấy Token' },
            '403': { description: 'Token hết hạn hoặc không hợp lệ' }
          }
        }
      }
    }
  },
  apis: [], // Để trống mảng này để Swagger không đi quét comment nữa
};

const swaggerSpec = swaggerJsdoc(swaggerOptions);
// Sửa lại dòng này trong src/server.ts
app.use(
  '/api-docs',
  swaggerUi.serve,
  swaggerUi.setup(swaggerSpec, {
    customCssUrl: "https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/4.15.5/swagger-ui.min.css",
    customJs: [
      "https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/4.15.5/swagger-ui-bundle.js",
      "https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/4.15.5/swagger-ui-standalone-preset.js",
    ],
  })
);

// --- CÁC API CỦA BẠN (CODE SẠCH SẼ KHÔNG CẦN COMMENT) ---

app.get('/api/health', (req: Request, res: Response) => {
    res.json({ 
        status: 'ok', 
        message: '🚀 SmartHire Node.js Service đang chạy!',
        timestamp: new Date().toISOString()
    });
});

app.get('/api/protected', requireAuth, (req: AuthRequest, res: Response) => {
    res.json({
        message: '🎉 Chúc mừng! Bạn đã vượt qua cửa bảo vệ của Node.js!',
        userInfo: req.user
    });
});

// Bật server
app.listen(port, () => {
    console.log(`🚀 Server đang chạy tại http://localhost:${port}`);
    console.log(`📚 Xem Swagger tại http://localhost:${port}/api-docs`);
});
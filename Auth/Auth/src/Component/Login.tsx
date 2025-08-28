import { Form, Input, Button, message, Typography } from "antd";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { useState } from "react";
import { useNavigate } from "react-router-dom";


interface TokenPayload {
  Role: string;
  id:string;
}

const Login: React.FC = () => {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const onFinish = async (values: any) => {
    setLoading(true);
    try {
      const response = await axios.post("http://localhost:5049/api/Auth/login", {
        email: values.Email,
        password: values.Password,
      });

      const { accessToken, refreshToken } = response.data;

      const decoded: TokenPayload = jwtDecode(accessToken);
      const role =decoded.Role
      const userId=decoded.id
      console.log(decoded)

      localStorage.setItem("accessToken", accessToken);
      localStorage.setItem("refreshToken", refreshToken);
      localStorage.setItem("role", role);
      localStorage.setItem("Id",userId);



      message.success("Logged in successfully");

      if (role === "Admin") {
        navigate("/admin");
      } 
      else {
        navigate("/student");
      }
    } catch (err) {
      console.error(err);
      message.error("Login failed");
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Typography.Title level={1} style={{ textAlign: "center" }}>Login</Typography.Title>
      <Form onFinish={onFinish} layout="vertical">
        <Form.Item name="Email" label="Email" rules={[{ required: true}]}>
          <Input placeholder="Enter Email" />
        </Form.Item>

        <Form.Item name="Password" label="Password" rules={[{ required: true }]}>
          <Input.Password placeholder="Enter Password" />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit" loading={loading}>
            Login
          </Button>
        </Form.Item>
      </Form>
    </>
  );
};

export default Login;

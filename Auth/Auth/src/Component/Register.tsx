import { Form, Input, Button, message, Typography, Select } from "antd";
import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const {Option}=Select;

const Register: React.FC = () => {
  const [loading, setLoading] = useState(false);
  const navigate=useNavigate();

  const onFinish = async (values: any) => {
    console.log(values);
    setLoading(true);
    try {
      await axios.post("http://localhost:5049/api/Auth/register", {
        email: values.Email,
        password: values.Password,
        role:values.role
      });

      message.success("Registered successfully");
      navigate("/login")
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
    } catch (err) {
      message.error("Registration failed");
    } finally {
      setLoading(false);
    }
  };

  return (
    <><Typography.Title level={1} style={{textAlign:"center"}}>Register</Typography.Title>
    <Form onFinish={onFinish} layout="vertical">
      <Form.Item name="Email" label="Email" rules={[{ required: true }]}>
        <Input placeholder="Enter Email:" />
      </Form.Item>

      <Form.Item name="Password" label="Password" rules={[{ required: true }]}>
        <Input.Password placeholder="Enter Password" />
      </Form.Item>

       <Form.Item name="role" label="Role" rules={[{ required: true }]}>
          <Select placeholder="Select Role">
            <Option value="Admin">Admin</Option>
            <Option value="Student">Student</Option>
          </Select>
        </Form.Item>

      <Form.Item>
        <Button type="primary" htmlType="submit" loading={loading}>
          Register
        </Button>
      </Form.Item>
    </Form></>
  );
};

export default Register;

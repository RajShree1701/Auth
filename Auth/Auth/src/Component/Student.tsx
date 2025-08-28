import { Form, Input, Typography, Button, message, Table } from "antd";
import axios from "axios";
import { useEffect, useState } from "react";

interface StudentState {
  Name: string;
  Email: string;
  Password: string;
  Class: string;
}

const Student = () => {
  const [students, setStudents] = useState<StudentState[]>([]);
  const [form] = Form.useForm();

  const columns = [
    { title: "Name", dataIndex: "Name" },
    { title: "Email", dataIndex: "Email" },
    { title: "Password", dataIndex: "Password" },
    { title: "Class", dataIndex: "Class" },
  ];

  const fetchStudents = async (id:string) => {
    try {
      const token = localStorage.getItem("accesstoken");

      const res = await axios.get(
        `http://localhost:5049/api/Student/${id}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      setStudents([res.data]);
      console.log(res.data);
    } catch (err) {
      message.error("Failed to fetch")
    }
  };

  const handleSubmit = async (values: StudentState) => {
    try {
      const token = localStorage.getItem("accessToken");

      const res = await axios.post(
        "http://localhost:5049/api/Student",
        values,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      setStudents((prev) => [...prev, res.data]);
      message.success("Student created successfully");
      form.resetFields();
    } catch (err) {
      console.error("Error:", err);
      message.error("Failed to create student");
    }
  };

  useEffect(() => {
  const id = localStorage.getItem("studentId");
  if (id) {
    fetchStudents(id);
  } else {
    message.error("Student ID not found");
  }
}, []);


  return (
    <div style={{ maxWidth: 500, margin: "0 auto" }}>
      <Typography.Title level={2}>Add Student</Typography.Title>

      <Form form={form} layout="vertical" onFinish={handleSubmit}>
        <Form.Item
          label="Name"
          name="Name"
          rules={[{ required: true, message: "Please enter name" }]}
        >
          <Input placeholder="Enter Name" />
        </Form.Item>

        <Form.Item
          label="Email"
          name="Email"
          rules={[{ required: true, message: "Please enter email" }]}
        >
          <Input placeholder="Enter Email" />
        </Form.Item>

        <Form.Item
          label="Password"
          name="Password"
          rules={[{ required: true, message: "Please enter password" }]}
        >
          <Input.Password placeholder="Enter Password" />
        </Form.Item>

        <Form.Item
          label="Class"
          name="Class"
          rules={[{ required: true, message: "Please enter class" }]}
        >
          <Input placeholder="Enter Class" />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit">
            Add Student
          </Button>
        </Form.Item>

        <Table columns={columns} dataSource={students} />
      </Form>
    </div>
  );
};

export default Student;

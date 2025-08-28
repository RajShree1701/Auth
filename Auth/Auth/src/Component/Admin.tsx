
import { Button, Form, Input, message, Table } from "antd";
import { useEffect, useState } from "react";
import AxiosInstance from "./AxiosInstance";

interface AdminState {
  adminName: string;
  email: string;
  password: string;
  role:string;
}

const Admin = () => {
  const [form]=Form.useForm();
  const [admin,setAdmin]=useState<AdminState[]>([])

  const columns=[
    {title:"Admin Name", dataIndex:"adminName"},
    {title:"Email", dataIndex:"email"},
    {title:"Password", dataIndex:"password"},
    {title:"Role", dataIndex:"role"}
  ]

  const fetchAdmin=async()=>{
    try{
      const res=await AxiosInstance.get("/Admin");
      setAdmin(res.data);
      console.log(res.data);
    }
    catch(err)
    {
      console.log(err);
      message.error("failed");
    }
  }

  const handleSubmit=async(values:AdminState)=>{
    try{
      const res=await AxiosInstance.post("/Admin",
        values
      );
      console.log(values)
      setAdmin((prev)=>[...prev,res.data]);
      form.resetFields();
    }
    catch(err)
    {
      console.log(err);
      message.error("failed");
    }
  };

  useEffect(()=>{
    fetchAdmin()
  },[])

  return (
    <div>
      <Form
      form={form}
      onFinish={handleSubmit}
      >
        <Form.Item label="Name" name="AdminName">
          <Input placeholder="Enter Name:"/>
        </Form.Item>

        <Form.Item label="Email" name="Email">
          <Input placeholder="Enter Email:"/>
        </Form.Item>

        <Form.Item label="Password" name="Password">
          <Input placeholder="Enter Password"/>
        </Form.Item>

        <Form.Item label="Role" name="Role">
          <Input placeholder="Enter Role"/>
        </Form.Item>

       <Form.Item>
          <Button type="primary" htmlType="submit">
            Add Admin
          </Button>
        </Form.Item>
      </Form>

      <Table columns={columns} dataSource={admin} />
    </div>
  );
};

export default Admin;

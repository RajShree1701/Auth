import { BrowserRouter,Route, Routes } from "react-router-dom"
import Register from "./Component/Register"
import Login from "./Component/Login"
import Admin from "./Component/Admin"
import Student from "./Component/Student"
// import ProtectedRoute from "./Component/ProtectedRoute"
// import { useState } from "react"
// import Profile from "./Component/Profile"


const App = () => {
  // const [isAuthenticated, setAuthenticated]=useState(false);
  return (
    <div>
      <BrowserRouter>
      <Routes>
        <Route path="/" element={<Register/>}/>
        <Route path="login" element={<Login/>}/>
        {/* <Route element={<ProtectedRoute isAuthenticated={isAuthenticated}/>}> */}
        <Route path="admin" element={<Admin/>}/>
        <Route path="student" element={<Student/>}/>
        {/* </Route> */}
        {/* <Route path="profile" element={<Profile/>}/> */}
      </Routes>
      </BrowserRouter>
    </div>
  )
}

export default App

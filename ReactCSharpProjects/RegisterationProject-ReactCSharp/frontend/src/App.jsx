import { useState } from "react";
import axios from "axios";
import './App.css';
function App() {
  const [form, setForm] = useState({ name: "", email: "", password: "" });
  const [message, setMessage] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post("https://localhost:7005/api/create-student", form);
      setMessage(response.data);
    } catch (error) {
      setMessage("Error creating user");
    }
  };

  return (
    <div className="container">
      <h1 className="title">User Registration</h1>
      <form onSubmit={handleSubmit} className="form-input">
        <input className="input" type="text" placeholder="Name" onChange={(e) => setForm({ ...form, name: e.target.value })} />
        <input className="input" type="email" placeholder="Email" onChange={(e) => setForm({ ...form, email: e.target.value })} />
        <input className="input" type="password" placeholder="Password" onChange={(e) => setForm({ ...form, password: e.target.value })} />
        <button className="button" type="submit">Register</button>
      </form>
      <p>{message}</p>
    </div>
  );
}

export default App;

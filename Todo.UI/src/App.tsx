import { Routes, BrowserRouter, Route, Navigate } from "react-router-dom";
import { ThemeProvider } from "styled-components";
import Login from "./pages/login";
import BaseLayout from "./layouts/baseLayout";
import Dashboard from "./pages/dashboard";
import Register from "./pages/register";
import { Default } from "./types/themes";
import Board from "./pages/board";
import Profile from "./pages/profile";

function App() {
  return (
    <ThemeProvider theme={Default}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Navigate to="/login" />} />
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="/" element={<BaseLayout />}>
            <Route path="home" element={<Dashboard />} />
            <Route path="board/:id" element={<Board />} />
            <Route path="profile" element={<Profile />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;

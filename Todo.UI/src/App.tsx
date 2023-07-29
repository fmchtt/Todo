import { Routes, BrowserRouter, Route, Navigate } from "react-router-dom";
import { ThemeProvider } from "styled-components";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import BaseLayout from "./layouts/baseLayout";
import { Default } from "./types/themes";
import { lazy } from "react";

import Login from "./pages/login";
import Register from "./pages/register";
import RecoverPassword from "./pages/recoverPassword";
import Invite from "./pages/invite";

const Dashboard = lazy(() => import("./pages/dashboard"));
const Board = lazy(() => import("./pages/board"));
const Profile = lazy(() => import("./pages/profile"));
const Boards = lazy(() => import("./pages/boards"));
const Tasks = lazy(() => import("./pages/tasks"));

function App() {
  return (
    <ThemeProvider theme={Default}>
      <ToastContainer
        position="bottom-right"
        autoClose={3000}
        newestOnTop
        closeOnClick
        pauseOnFocusLoss
        pauseOnHover
        theme="dark"
      />
      <BrowserRouter>
        <Routes>
          <Route path="/" index={true} element={<Navigate to="/login" />} />
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="invite/:id" element={<Invite />} />
          <Route path="password/reset" element={<RecoverPassword />} />
          <Route path="/" element={<BaseLayout />}>
            <Route path="home" element={<Dashboard />} />
            <Route path="board/:id" element={<Board />} />
            <Route path="profile" element={<Profile />} />
            <Route path="boards" element={<Boards />} />
            <Route path="tasks" element={<Tasks />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;

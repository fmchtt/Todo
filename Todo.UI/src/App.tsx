import { Routes, BrowserRouter, Route, Navigate } from "react-router-dom";
import { ThemeProvider, DefaultTheme } from "styled-components";
import Login from "./pages/login";
import BaseLayout from "./layouts/baseLayout";
import Dashboard from "./pages/dashboard";

function App() {
  const theme: DefaultTheme = {
    primaryColor: "#0B5351",
    colors: {
      900: "#0A2E36",
      800: "#0A3435",
      700: "#084E4E",
      600: "#095D5B",
      500: "#0B5351",
      400: "",
      300: "",
      200: "#075B5B",
      100: "#03928F",
    },
    font: {
      bold: "#fff",
      medium: "#fff",
      thin: "#fff",
    },
    gradients: {
      full: "linear-gradient(180deg, #092327 45.31%, #000000 100%);",
      semi: "linear-gradient(180deg, #095D5B 0%, #092327 100%);",
    },
  };

  return (
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Navigate to="/login" />} />
          <Route path="login" element={<Login />} />
          <Route path="/" element={<BaseLayout />}>
            <Route path="home" element={<Dashboard />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;

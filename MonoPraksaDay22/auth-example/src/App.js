import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import LoginPage from './LoginPage';
import { useEffect } from 'react';
import ListPage from './ListPage';
import RegisterPage from './RegisterPage';
function App() {

  useEffect(() => {
    if(window.location.pathname === "/login" || window.location.pathname === "/register") return;
    
    if(!localStorage.getItem("AuthToken")) {
      window.location.href = "/login";
    }
  }, []);

  const router = createBrowserRouter([
    {
      path: "/login",
      element: (
        <LoginPage />
      ),
    },
    {
      path: "/register",
      element: (
        <RegisterPage />
      ),
    },
    {
      path: "/",
      element: (
        <ListPage/>
      )
    },
    {
      path: "*",
      element: (
        <h1>404 Not Found</h1>
      )
    }
  ]);
  
  
  return (
    <RouterProvider router={router} />
  );
}

export default App;

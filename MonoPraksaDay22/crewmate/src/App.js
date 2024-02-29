import './App.css';
import HomePage from './Pages/HomePage';
import EditPage from './Pages/EditPage';
import {
  createBrowserRouter,
  RouterProvider,
  Link
} from "react-router-dom";
import AddPage from './Pages/AddPage';
import EditExperiencePage from './Pages/EditExperiencePage';
import { Outlet } from 'react-router-dom';
import ListPage from './Pages/ListPage';


function App() {

  const router = createBrowserRouter([
    {
      path: "/",
      element: (
        <HomePage />
      ),
    },
    {
      path: "/edit/:id",
      element: (
        <>
        <EditPage />
        <Outlet />
        </>
      ),
      children: [
        {
          path: "addExperience/",
          element: (
            <EditExperiencePage/>
          )
        }
      ]
    },
    {
      path: "/add",
      element: (
        <AddPage />
      )
    },
    {
      path: "/list",
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

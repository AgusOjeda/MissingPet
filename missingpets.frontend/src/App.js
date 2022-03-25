import { Text } from "@chakra-ui/react";
import Navbar from "./Navbar";
import {
  BrowserRouter as Router,
  Routes as ReactRoutes,
  Route as ReactRoute,
} from "react-router-dom";
import Read from "./Helper/Read";
import Create from "./Helper/Create";
import Update from "./Helper/Update";
import Delete from "./Helper/Delete";
import Table from "./Helper/Table";
{
  /*"Read", "Update", "Delete"*/
}

const Links = [
  { display: "Create", path: "/create", element: <Create /> },
  { display: "Read", path: "/read", element: <Table /> },
  { display: "Update", path: "/update", element: <Update /> },
  { display: "Delete", path: "/delete", element: <Delete /> },
];

function App() {
  return (
    <div className="App">
      <Router>
        <Navbar Links={Links} />
        <ReactRoutes>
          {Links.map((link) => (
            <ReactRoute
              key={link.display}
              path={link.path}
              element={link.element}
            />
          ))}
        </ReactRoutes>
      </Router>
    </div>
  );
}

export default App;

import { Fragment } from "react";
import Navbar from "./navbar";
import { BrowserRouter as Router } from "react-router-dom";

const Layout = () => {
  return (
    <Fragment>
      <Router>
        <Navbar />
      </Router>
    </Fragment>
  );
};

export default Layout;

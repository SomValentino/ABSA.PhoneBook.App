import React from "react";
import { Route, Switch, Link } from "react-router-dom";
import classes from "./navbar.module.css";
import Book from "../PhoneBook/book";
import Entry from "../PhoneBook/entry";
import CreateBook from "../PhoneBook/createbook";
import CreateEntry from "../PhoneBook/createentry";

const Navbar = () => {
  return (
    <>
      <header className={classes.header}>
        <h1>PhoneBook</h1>
        <nav>
          <ul>
            <li></li>
          </ul>
        </nav>
      </header>
      <Switch>
        <Route exact path="/">
          <Book />
        </Route>
        <Route exact path="/entry/:phonebookId/:name">
          <Entry />
        </Route>
        <Route exact path="/createbook">
          <CreateBook />
        </Route>
        <Route exact path="/createentry">
          <CreateEntry />
        </Route>
      </Switch>
    </>
  );
};

export default Navbar;

import React from "react";
import { Route, Switch, Link } from "react-router-dom";
import classes from "./navbar.module.css";
import Book from "../PhoneBook/book";
import Entry from "../PhoneBook/entry";
import CreateBook from "../PhoneBook/createbook";
import CreateEntry from "../PhoneBook/createentry";
import UpdateEntry from "../PhoneBook/updateentry"
import UpdateBook from "../PhoneBook/updatebook"

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
        <Route exact path="/updatebook/:phonebookId">
          <UpdateBook />
        </Route>
        <Route exact path="/createentry/:phonebookId/:name">
          <CreateEntry />
        </Route>
        <Route exact path="/updateentry/:phonebookId/:name/:entryId">
          <UpdateEntry />
        </Route>
      </Switch>
    </>
  );
};

export default Navbar;

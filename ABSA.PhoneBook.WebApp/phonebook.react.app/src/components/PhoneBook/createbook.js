import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Button } from "react-bootstrap";

import useFetch from "../../hook/usefetch";
import Card from '../UI/card'

const CreateBook = () => {
  const [name, setName] = useState(null);
  const [payload, setPayload] = useState(null);
  const { data, isloading, error } = useFetch(
    "/api/phonebook",
    "POST",
    payload
  );
  const history = useHistory()
  return (
    <Card>
      <div>
        <h4>Create phonebook</h4>
        {!payload && isloading && !error ? (
          <form>
            <div className="form-group row">
              
              <div className="col-md-6">
                <input
                  type="text"
                  name="name"
                  value={name}
                  onChange={e => setName(e.target.value)}
                  className="form-control"
                  placeholder="Enter phonebook name"
                />
              </div>
            </div><br/>
            {!name ? (
              <Button type="submit" disabled>
                Submit
              </Button>
            ) : (
              <Button type="submit" onClick={() => setPayload({ name })}>
                Submit
              </Button>
            )}
          </form>
        ) : payload && !isloading && !error ? (
          <div>
            <p>Successfully created phonebook</p>
            <div className="row">
              <div className="col-md-3">
                <Button type="button" onClick={() => history.push("/")}>
                  Back
                </Button>
              </div>
            </div>
          </div>
        ) : (
          <div>
            <p>An error occured</p>
            <div className="row">
              <div className="col-md-3">
                <Button type="button" onClick={() => history.push("/")}>
                  Back
                </Button>
              </div>
            </div>
          </div>
        )}
      </div>
    </Card>
  );
};

export default CreateBook;

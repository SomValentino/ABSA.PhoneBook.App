import React, { useState, useEffect } from "react";
import { useHistory, useParams } from "react-router-dom";
import { Button } from "react-bootstrap";

import useFetch from "../../hook/usefetch";
import Card from "../UI/card";

const UpdateEntry = () => {
  const [name, setName] = useState(null);
  const [valid, setValid] = useState(name);
  const { phonebookId } = useParams();
  const [payload, setPayload] = useState(null);
  const [getError, setGetError] = useState(null)
  const { REACT_APP_BaseUrl } = process.env;

  const { data, isloading, error } = useFetch(
    `/api/phonebook/${phonebookId}`,
    "PUT",
    payload
  );
  const history = useHistory();

  const GetEntry = async () => {
    try {
      var response = await fetch(
        `${REACT_APP_BaseUrl}/api/phonebook/${phonebookId}`
      );

      var responseData = await response.json();

      setName(responseData.name);
    } catch (error) {
      setGetError(error.message)
      throw new Error(error.message);
    }
  };

  useEffect(() => {
    GetEntry();
  }, []);

  return (
    <Card>
      <div>
        <h4>Update PhoneBook</h4>
        {!payload && isloading && !error ? (
          <form>
            <div className="form-group row">
              <div className="col-md-6">
                <input
                  type="text"
                  name="name"
                  value={name}
                  onChange={e => {
                    e.preventDefault();
                    setName(e.target.value);
                    setValid(name);
                  }}
                  className="form-control"
                  placeholder="Enter phonebook name"
                  required
                />
              </div>
            </div>
            <br/>
            {!valid ? (
              <div className="row">
                <div className="col-md-3">
                  <Button type="submit" disabled>
                    Submit
                  </Button>
                </div>
                <div className="col-md-3">
                  <Button
                    type="button"
                    variant="danger"
                    onClick={() =>
                      history.push(`/`)
                    }
                  >
                    Back
                  </Button>
                </div>
              </div>
            ) : (
              <div className="row">
                <div className="col-md-3">
                  <Button
                    type="submit"
                    onClick={e => {
                      e.preventDefault();

                      setPayload({
                        name
                      });
                    }}
                  >
                    Submit
                  </Button>
                </div>
                <div className="col-md-3">
                  <Button
                    type="button"
                    variant="danger"
                    onClick={() =>
                      history.push(`/`)
                    }
                  >
                    Back
                  </Button>
                </div>
              </div>
            )}
          </form>
        ) : payload && !isloading && !error ? (
          <div>
            <p>Successfully created phonebook</p>
            <br />
            <Button
              type="button"
              onClick={() =>
                history.push(`/`)
              }
            >
              Back
            </Button>
          </div>
        ) : getError ? (
          <div>
            <p>An error occured : {getError}</p>
            <br />
            <Button
              type="button"
              onClick={() =>
                history.push(`/`)
              }
            >
              Back
            </Button>
          </div>
        ) : (
          <div>
            <p>An error occured : {error}</p>
            <br />
            <Button
              type="button"
              onClick={() =>
                history.push(`/`)
              }
            >
              Back
            </Button>
          </div>
        )}
      </div>
    </Card>
  );
};

export default UpdateEntry;

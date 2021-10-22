import React, { useState, useEffect } from "react";
import { useHistory,useParams } from "react-router-dom";
import { Button } from "react-bootstrap";

import useFetch from "../../hook/usefetch";
import Card from "../UI/card";

const CreateBookEntry = () => {
  const [name, setName] = useState(null);
  const [phoneNumber, setPhoneNumber] = useState(null)
  const {phonebookId, name: phonebookName } = useParams()
  const [payload, setPayload] = useState(null);
  const { data, isloading, error } = useFetch(
    `/api/phonebook/${phonebookId}/entry`,
    "POST",
    payload
  );
  const history = useHistory();
  return (
    <Card>
      <div>
        <h4>Create Entry</h4>
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
            </div>
            <br />
            <div className="form-group row">
              <div className="col-md-6">
                <input
                  type="text"
                  name="phoneNumber"
                  value={phoneNumber}
                  onChange={e => setPhoneNumber(e.target.value)}
                  className="form-control"
                  placeholder="Enter phoneNumber"
                />
              </div>
            </div>
            <br />
            {!name ? (
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
                      history.push(`/entry/${phonebookId}/${phonebookName}`)
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
                    onClick={() =>
                      setPayload({
                        name,
                        phoneNumber,
                        phoneBookId: phonebookId
                      })
                    }
                  >
                    Submit
                  </Button>
                </div>
                <div className="col-md-3">
                  <Button
                    type="button"
                    variant="danger"
                    onClick={() =>
                      history.push(`/entry/${phonebookId}/${phonebookName}`)
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
                history.push(`/entry/${phonebookId}/${phonebookName}`)
              }
            >
              Back
            </Button>
          </div>
        ) : (
          <div>
            <p>An error occured</p>
            <br />
            <Button
              type="button"
              onClick={() =>
                history.push(`/entry/${phonebookId}/${phonebookName}`)
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

export default CreateBookEntry;

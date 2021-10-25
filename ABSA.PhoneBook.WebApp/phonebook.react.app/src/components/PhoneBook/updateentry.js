import React, { useState, useEffect } from "react";
import { useHistory, useParams } from "react-router-dom";
import { Button } from "react-bootstrap";

import useFetch from "../../hook/usefetch";
import Card from "../UI/card";

const UpdateBookEntry = () => {
  const [name, setName] = useState(null);
  const [phoneNumber, setPhoneNumber] = useState(null);
  const [valid, setValid] = useState(name && phoneNumber);
  const { phonebookId, name: phonebookName, entryId } = useParams();
  const [payload, setPayload] = useState(null);
  const [getError, setGetError] = useState(null);
  const { REACT_APP_BaseUrl } = process.env;

  const { data, isloading, error } = useFetch(
    `/api/phonebook/entry/${entryId}`,
    "PUT",
    payload
  );
  const history = useHistory();

  const GetEntry = async () => {
    try {
      var response = await fetch(
        `${REACT_APP_BaseUrl}/api/phonebook/entry/${entryId}`
      );

      if (
        response.status === 400 ||
        response.status === 404 ||
        response.status === 500
      ) {
        const badresponse = await response.json();
        throw new Error(badresponse.errorMessage);
      }

      if (response.status >= 200 && response.status >= 299)
        throw new Error(response.statusText);

      var responseData = await response.json();

      setName(responseData.name);
      setPhoneNumber(responseData.phoneNumber);
    } catch (error) {
      setGetError(error.message)
    }
  };

  useEffect(() => {
    GetEntry();
  }, []);

  return (
    <Card>
      <div>
        <h4>Update Entry</h4>
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
                    setValid(name && phoneNumber);
                  }}
                  className="form-control"
                  placeholder="Enter phonebook name"
                  required
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
                  onChange={e => {
                    e.preventDefault();
                    setPhoneNumber(e.target.value);
                    setValid(name && phoneNumber);
                  }}
                  className="form-control"
                  placeholder="Enter phoneNumber"
                  required
                  maxLength="10"
                />
              </div>
            </div>
            <br />
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
                    onClick={e => {
                      e.preventDefault();

                      setPayload({
                        name,
                        phoneNumber
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
        ) : getError ? (
          <div>
            <p>An error occured: {getError}</p>
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
            <p>An error occured: {error}</p>
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

export default UpdateBookEntry;

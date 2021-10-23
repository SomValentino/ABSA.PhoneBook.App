import React, { useState, useMemo } from "react";
import { useHistory, useParams } from "react-router-dom";
import { Button } from "react-bootstrap";

import PTable from "../Data/table";
import useFetch from "../../hook/usefetch";
import Card from "../UI/card";

const Entry = () => {
  const { phonebookId,name } = useParams();
  const history = useHistory();
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [proxySerach, setProxySerach] = useState("");
  const [search, setSearch] = useState(null);

  const { data: phonebookEntryData, isloading, error } = useFetch(
    `/api/phonebook/${phonebookId}/entries`,
    "GET",
    null,
    page,
    pageSize,
    search
  );

  const list = phonebookEntryData?.phoneBookEntries.map(bookEntry => {
    return {
      Name: bookEntry.name,
      PhoneNumber: bookEntry.phoneNumber,
      DateCreated: new Date(bookEntry.createdAt).toLocaleDateString("en-ZA"),
      Actions: (
        <>
          <Button
            onClick={() => history.push(`/updateentry/${phonebookId}/${name}/${bookEntry.id}`)}
            variant="warning"
          >
            Update
          </Button>
        </>
      )
    };
  });

  const data = useMemo(() => list, [phonebookEntryData]);
  const columns = React.useMemo(
    () => [
      {
        Header: "Name",
        accessor: "Name"
      },
      {
        Header: "PhoneNumber",
        accessor: "PhoneNumber"
      },
      {
        Header: "DateCreated",
        accessor: "DateCreated"
      },
      {
        Header: "Actions",
        accessor: "Actions"
      }
    ],
    []
  );
  if (isloading) return <div>loading...</div>;
  return (
    <Card>
      <div className="p-4 bg-white my-4 rounded shadow-xl grid">
        <h4>{`${name} - Entries`}</h4>
        <br />
        <div>
          <Button
            onClick={() => history.push(`/createentry/${phonebookId}/${name}`)}
          >
            Create new PhoneBook Entry
          </Button>
        </div>
        <br />
        {phonebookEntryData.phoneBookEntries &&
        phonebookEntryData.phoneBookEntries.length ? (
          <div>
            <div>
              <input
                type="text"
                value={proxySerach}
                onChange={e => {
                  const textValue = e.target.value;
                  setProxySerach(textValue);
                  if (!textValue) setSearch(null);
                }}
                placeholder="Enter name or phonenumber"
              />
              {"  "}
              <Button
                onClick={() => {
                  setSearch(proxySerach);
                  setPage(1);
                }}
              >
                Search
              </Button>
            </div>
            <PTable
              data={data}
              columns={columns}
              setPage={setPage}
              setPerPage={setPageSize}
              currentpage={page}
              perPage={pageSize}
              totalPage={phonebookEntryData?.total}
            />
          </div>
        ) : (
          <div>No entry avaliable. Click on button above to create entry</div>
        )}
      </div>
      <div style={{ width: "10%", margin: "auto" }}>
        <Button
          type="button"
          variant="danger"
          onClick={() => history.push("/")}
        >
          Back
        </Button>
      </div>
    </Card>
  );
};

export default Entry;

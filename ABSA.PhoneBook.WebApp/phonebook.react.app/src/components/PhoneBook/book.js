import React,{useState,useMemo,useEffect} from 'react'
import {useHistory} from 'react-router-dom'
import { Button,Modal } from "react-bootstrap";

import useFetch from '../../hook/usefetch'
import PTable from '../Data/table'
import Card from '../UI/card'

const Book = () => {
  const history = useHistory()
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(5)
  const [proxySerach, setProxySerach] = useState('')
  const [search, setSearch] = useState(null)
  const [deleteId, setDeleteId] = useState(null);
  const [phonebookId, setPhoneBookId] = useState(null);
  const [show, setShow] = useState(false);
  const [reload, setReload] = useState(false);
  const { REACT_APP_BaseUrl } = process.env;
  
  const {data: phonebookData, isloading,error} = useFetch('/api/phonebook','GET',null,page,pageSize,search,reload)

  const deletePhoneBook = async () => {
    try {
      const response = await fetch(
        `${REACT_APP_BaseUrl}/api/phonebook/${phonebookId}`,
        { method: "DELETE" }
      );

      setReload(true);
      setPhoneBookId(null);
      setDeleteId(null);
    } catch (error) {
      setPhoneBookId(null);
      setDeleteId(null);
      throw new Error(error.message);
    }
  };

  useEffect(() => {
    if (phonebookId) deletePhoneBook();
  }, [phonebookId]);

  const list = phonebookData?.phoneBooks.map(book => {
    return {
      PhoneBookName: book.name,
      DateCreated: new Date(book.createdAt).toLocaleDateString("en-ZA"),
      Actions: (
        <>
          <Button
            onClick={() => history.push(`/entry/${book.id}/${book.name}`)}
          >
            View
          </Button>{" "}
          <Button
            onClick={() => history.push(`/updatebook/${book.id}`)}
            variant="warning"
          >
            Update
          </Button>{" "}
          <Button
            onClick={() => {
              setDeleteId(book.id);
              setShow(true);
              setReload(false);
            }}
            variant="danger"
          >
            Delete
          </Button>
        </>
      )
    };
  })
  
  const data = useMemo(() => list, [phonebookData])
  const columns = React.useMemo(
    () => [
      {
        Header: "Name",
        accessor: "PhoneBookName"
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
      <Modal
        show={show}
        onHide={() => {
          setShow(false);
          setPhoneBookId(null);
          setDeleteId(null);
        }}
      >
        <Modal.Header closeButton>
          <Modal.Title>Delete PhoneBook</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Are you sure you want delete PhoneBook. All entries in the PhoneBook
          will be deleted
        </Modal.Body>
        <Modal.Footer>
          <Button
            variant="secondary"
            onClick={() => {
              setShow(false);
              setPhoneBookId(null);
              setDeleteId(null);
            }}
          >
            Close
          </Button>
          <Button
            variant="primary"
            onClick={() => {
              setPhoneBookId(deleteId);
              setShow(false);
              setPage(1);
            }}
          >
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
      <div className="p-4 bg-white my-4 rounded shadow-xl grid">
        <div>
          <Button onClick={() => history.push("/createbook")}>
            Create new PhoneBook
          </Button>
        </div>
        <br />
        {phonebookData && phonebookData.phoneBooks &&
        phonebookData.phoneBooks.length ? (
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
                placeholder="Enter phonebook name"
                className="form-control col-md-3"
              />
              <br />
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
              totalPage={phonebookData?.total}
            />
          </div>
        ) : (
          <div>
            No PhoneBook available. Click on the Button above to create one
          </div>
        )}
      </div>
    </Card>
  );
};

export default Book;

import React,{useState,useMemo} from 'react'
import {useHistory} from 'react-router-dom'
import { Button } from "react-bootstrap";

import useFetch from '../../hook/usefetch'
import PTable from '../Data/table'
import Card from '../UI/card'

const Book = () => {
  const history = useHistory()
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(5)
  const [proxySerach, setProxySerach] = useState('')
  const [search, setSearch] = useState(null)
  
  const {data: phonebookData, isloading,error} = useFetch('/api/phonebook','GET',null,page,pageSize,search)

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
            onClick={() => history.push(`/createentry/${book.id}/${book.name}`)}
            variant="warning"
          >
            Update
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
      <div className="p-4 bg-white my-4 rounded shadow-xl grid">
        <div>
          <Button onClick={() => history.push("/createbook")}>
            Create new PhoneBook
          </Button>
        </div>
        <br />
        { phonebookData ?
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
              <br/>
              <Button onClick={() => 
                                {
                                  setSearch(proxySerach)
                                  setPage(1)
                                }}>Search</Button>
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
          </div> : <div>No PhoneBook available. Click on the Button above to create one</div>
        }
      </div>
    </Card>
  );
};

export default Book;

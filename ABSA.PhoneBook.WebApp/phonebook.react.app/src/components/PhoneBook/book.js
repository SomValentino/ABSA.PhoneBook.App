import React,{useState,useMemo} from 'react'
import {useHistory} from 'react-router-dom'
import { Button } from "react-bootstrap";

import useFetch from '../../hook/usefetch'
import PTable from '../Data/table'

const Book = () => {
  const history = useHistory()
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(5)
  console.log(page)
  const {data: phonebookData, isloading,error} = useFetch('/api/phonebook','GET',null,page,pageSize)

  const list = phonebookData?.phoneBooks.map(book => {
    return {
      PhoneBookName : book.name,
      Actions: <Button onClick={() => history.push(`/entry/${book.id}`)}>View</Button>
    }
  })
  
  const data = useMemo(() => list, [phonebookData])
  const columns = React.useMemo(
    () => [
      {
        Header: "Name",
        accessor: "PhoneBookName"
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
    <div className="p-4 bg-white my-4 rounded shadow-xl grid">
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
  )
};

export default Book;

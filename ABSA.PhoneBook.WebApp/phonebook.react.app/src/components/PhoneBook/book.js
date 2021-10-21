import React,{useState,useMemo} from 'react'
import {useHistory} from 'react-router-dom'

import useFetch from '../../hook/usefetch'
import Table from '../Data/table'

const Book = () => {
  const history = useHistory()
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(10)
  const {data: phonebookData, isloading,error} = useFetch('/api/phonebook','GET',null,{page,pageSize})

  const list = phonebookData?.phoneBooks.map(book => {
    return {
      PhoneBookName : book.name,
      Actions: <button onClick={() => history.push(`/entry/${book.id}`)}>View</button>
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
      <Table
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

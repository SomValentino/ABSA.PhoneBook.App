import React from "react";
import { useTable, usePagination } from "react-table";

import Table from "react-bootstrap/Table";
import { Button } from "react-bootstrap";

function PTable({
  setPerPage,
  setPage,
  columns,
  data,
  currentpage,
  perPage,
  totalPage
}) {
  const {
    getTableProps,
    getTableBodyProps,
    headerGroups,
    prepareRow,
    page,
    // canPreviousPage,
    // canNextPage,
    pageOptions,
    // pageCount,
    // gotoPage,
    // nextPage,
    // previousPage,
    // setPageSize,
    // Get the state from the instance
    state: { pageIndex, pageSize }
  } = useTable(
    {
      columns,
      data,
      useControlledState: state => {
        return React.useMemo(
          () => ({
            ...state,
            pageIndex: currentpage
          }),
          [state, currentpage]
        );
      },
      initialState: { pageIndex: currentpage }, // Pass our hoisted table state
      manualPagination: true, // Tell the usePagination
      // hook that we'll handle our own data fetching
      // This means we'll also have to provide our own
      // pageCount.
      pageCount: totalPage
    },
    usePagination
  );

  return (
    <>
      <Table {...getTableProps()} className="table-fixed">
        <thead>
          {headerGroups.map(headerGroup => (
            <tr {...headerGroup.getHeaderGroupProps()}>
              {headerGroup.headers.slice(0, 1).map(column => (
                <th
                  {...column.getHeaderProps()}
                  className="px-1 py-4 bg-red-100 capitalize w-96 text-left"
                >
                  {column.render("Header")}
                </th>
              ))}
              {headerGroup.headers.slice(1).map(column => (
                <th
                  {...column.getHeaderProps()}
                  className="py-4 bg-red-100 capitalize w-1/6 text-left"
                >
                  {column.render("Header")}
                </th>
              ))}
            </tr>
          ))}
        </thead>
        <tbody {...getTableBodyProps()}>
          {page.map((row, i) => {
            prepareRow(row);
            return (
              <tr {...row.getRowProps()}>
                {row.cells.map(cell => {
                  return (
                    <td
                      {...cell.getCellProps()}
                      className="truncate p-1 border-b-2"
                    >
                      {cell.render("Cell")}
                    </td>
                  );
                })}
              </tr>
            );
          })}
        </tbody>
      </Table>

      <div className="flex justify-between bg-red-100 p-4">
        <Button
          onClick={() => {
            setPage(1);
          }}
          disabled={currentpage === 1}
          variant="danger"
        >
          first
        </Button>{" "}
        <Button
          onClick={() => {
            setPage(s => (s === 0 ? 0 : s - 1));
          }}
          disabled={currentpage === 1}
          variant="danger"
        >
          prev
        </Button>{" "}
        <Button
          onClick={() => {
            setPage(s => s + 1);
            console.log(currentpage);
          }}
          disabled={currentpage >= Math.ceil(totalPage / perPage)}
          variant="success"
        >
          next
        </Button>{" "}
        <Button
          onClick={() => {
            setPage(Math.ceil(totalPage / perPage));
          }}
          disabled={currentpage >= Math.ceil(totalPage / perPage)}
          variant="success"
        >
          last
        </Button>{" "}
        <span>
          Page{" "}
          <strong>
            {pageIndex} of {Math.ceil(totalPage / perPage)}
          </strong>{" "}
        </span>
        <span>
          | Go to page:{" "}
          <input
            type="number"
            defaultValue={pageIndex}
            min="1"
            max={Math.ceil(totalPage / perPage)}
            onChange={e => {
              let page = e.target.value ? Number(e.target.value) : 1;
              page = page > Math.ceil(totalPage / perPage) ? Math.ceil(totalPage / perPage) : page
              setPage(page);
            }}
            className="w-20 border-2 rounded px-2"
          />
        </span>{" "}
        <select
          value={perPage}
          onChange={e => {
            // setPageSize(Number(e.target.value));
            console.log(Number(e.target.value));
            setPerPage(Number(e.target.value));
          }}
        >
          {[5, 10, 20].map(pageSize => (
            <option key={pageSize} value={pageSize}>
              Show {pageSize}
            </option>
          ))}
        </select>
      </div>
    </>
  );
}

export default PTable;

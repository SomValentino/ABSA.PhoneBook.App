import {useState,useEffect,useCallback} from 'react'
const useFetch = (path,method= 'GET',payload = null,page=null,pageSize=null,search=null,reload=false) => {
    const [data, setData] = useState(null)
    const [isloading, setLoading] = useState(true)
    const [error, setError] = useState(null)

    const { REACT_APP_BaseUrl } = process.env;

    const buildUrl = () => {
        let fullPath = REACT_APP_BaseUrl + path

        if(page && pageSize) fullPath +=`?page=${page}&pageSize=${pageSize}`
        
        if(search) fullPath += `&searchCriteria=${search}`

        return fullPath
    }

    const GetData =  useCallback(async () => {
        const url = buildUrl()

        try {
            if((method === 'POST' || method === 'PUT') && !payload) return
            var response = await fetch(url,{
                method:method,
                headers: {"Content-Type": "application/json"},
                body: payload ? JSON.stringify(payload) : null
            })
            
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
            
            if (method !== "PUT" && method !== "DELETE"){
              const responseData = await response.json();

              if (responseData) setData(responseData);
            } 
            setLoading(false)
            setError(null)

        } catch (error) {
            setError(error.message)
            console.log(error.message)
            setLoading(false)
        }
    },[path,page,pageSize,search,payload,reload])

    useEffect(() => {
        GetData()
    }, [path,GetData,page,pageSize,search,payload,reload])

    return {data,isloading,error}

}

export default useFetch
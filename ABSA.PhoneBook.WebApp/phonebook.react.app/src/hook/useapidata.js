import {useState} from 'react'
const UseApiData = (path,payload,queryParams = null) => {
    const [data, setData] = useState(null)
    const [isloading, setLoading] = useState(false)
    const [isError, setIsError] = useState(false)

    const { REACT_APP_BaseUrl } = process.env;

    const buildUrl = () => {
        let fullPath = REACT_APP_BaseUrl + path
        if(queryParams){
           let result = []
           for(const key in queryParams){
              result.push(`key=${queryParams[key]}`)
           }

           const queryPath = `?${result.join('&')}`
           fullPath += queryPath
        }

        return fullPath
    }

    const GetData = async () => {
        const url = buildUrl()

        try {
            
        } catch (error) {
            
        }
    }

}
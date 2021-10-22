import React,{useState,useEffect} from 'react'
import {useHistory} from 'react-router-dom'
const CreateEntry = () => {
    const [name, setName] = useState(null)
    return (
      <div>
        <form>
          <div className="form-group row">
            <div className="col-md-3">
              <label htmlFor="name" className="control-label col-md-3"></label>
            </div>
            <div className="col-md-3">
                <input type="text" name="name" value={name} onChange={e => setName(e.target.value)} />
            </div>
          </div>
        </form>
      </div>
    );
}

export default CreateEntry
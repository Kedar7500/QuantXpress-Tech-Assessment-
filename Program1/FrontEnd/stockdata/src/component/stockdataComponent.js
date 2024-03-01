import React ,{useState,useEffect} from 'react';
import axios from 'axios';

const url="https://localhost:7154/api/StockData/";

export default function StockDataComponent(){

    const[symbol, setSymbol]=useState('');
    const[stockData,setStockdata]=useState(null);
    const[errorMessage,setErrorMessage]=useState('');

    const handleSubmit= async(e)=>{
        e.preventDefault();
        try{
            const resp = await axios.get(url+symbol,{
                headers:{
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                },
            });
            setStockdata(resp.data)
        }catch(error){
            setErrorMessage("Symbol not found");
        }

    };

    useEffect(()=>{
        const token = localStorage.getItem('token');
        // if (!token) {
        //     setErrorMessage('You dont have access the stock data.');
        // }
    },[]);

    return(
        <div>
            <h2>Stock Data</h2>
            <form>
                <input type="text" placeholder="enter your symbol" value={symbol} onChange={(e)=>setSymbol(e.target.value)} ></input>
                <button type="submit">Live Price</button>
            </form>
            <p>{errorMessage}</p>
            <div>
                <h2>{symbol}</h2>
                <p>Date: {stockData && stockData[0]}</p>
                <p>Open Price: {stockData && stockData[1]}</p>
                <p>High Price: {stockData && stockData[2]}</p>
                <p>Low Price: {stockData && stockData[3]}</p>
                <p>Close Price: {stockData && stockData[4]}</p>
                <p>Volume: {stockData && stockData[5]}</p>
                <p>OI: {stockData && stockData[6]}</p>
            </div>
        </div>
        

    );

}
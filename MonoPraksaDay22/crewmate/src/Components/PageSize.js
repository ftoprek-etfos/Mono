export default function PageSize({paging, handlePageSizeChange}) {
    
    return (
        <div style={{display: 'flex', gap: '10px', justifyContent: 'center', alignItems: 'flex-start'}}>  
        <p>Page {paging.pageNumber} of {paging.totalPages}</p>               
        <select name="pageSize" id="pageSize" onChange={handlePageSizeChange}>
            <option value="3">3</option>
            <option value="5">5</option>
            <option value="10">10</option>
            </select>
            </div>
    );
}
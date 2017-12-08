package uy.com.searchandfind.query;

import uy.com.searchandfind.Response;

/**
 * Created by strat on 24/10/2017.
 */

public class QueryResponse extends Response {
    private QueryDTO QueryDTO;

    public QueryResponse(String message){
        super(message);
    }
    public QueryDTO getQueryDTO() {
        return QueryDTO;
    }

    public void setQueryDTO(QueryDTO queryDTO) {
        QueryDTO = queryDTO;
    }
}

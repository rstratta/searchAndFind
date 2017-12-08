package uy.com.searchandfind.client;

import uy.com.searchandfind.Response;

/**
 * Created by strat on 30/09/2017.
 */

public class ClientResponse extends Response {

    private ClientDTO ClientDTO;


    public ClientResponse(){
        super();
    }
    public ClientResponse(String message){
        super(message);
    }

    public ClientDTO getClientDTO() {
        return ClientDTO;
    }

    public void setClientDTO(ClientDTO clientDTO) {
        this.ClientDTO = clientDTO;
    }
}

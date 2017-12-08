package uy.com.searchandfind.service;

import uy.com.searchandfind.Response;

/**
 * Created by strat on 30/10/2017.
 */

public interface AsyncResponse {

    void processResponse(Response response);
}

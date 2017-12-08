package uy.com.searchandfind.service.backend;

import uy.com.searchandfind.Response;

/**
 * Created by strat on 30/09/2017.
 */

public interface ResponseBuilder<R extends Response> {

    R buildResponse(String jsonResponse);

    R buildBadResponse(String message);
}

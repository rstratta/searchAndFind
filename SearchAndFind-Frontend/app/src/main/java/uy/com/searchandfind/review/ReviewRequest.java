package uy.com.searchandfind.review;

import uy.com.searchandfind.Request;

/**
 * Created by Luis on 5/11/2017.
 */

public class ReviewRequest extends Request {

    private int Points;
    private String TenderId;

    public int getPoints() {
        return Points;
    }

    public void setPoints(int points) {
        Points = points;
    }

    public String getTenderId() {
        return TenderId;
    }

    public void setTenderId(String tenderId) {
        TenderId = tenderId;
    }


}

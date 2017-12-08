package uy.com.searchandfind.service.cloud.location;

/**
 * Created by strat on 12/10/2017.
 */

public class LocationDTO {
    private double length;
    private double latitude;

    public LocationDTO(){}
    public LocationDTO(double latitude, double length){
        this.length=length;
        this.latitude=latitude;
    }
    public double getLength() {
        return length;
    }

    public void setLength(double length) {
        this.length = length;
    }

    public double getLatitude() {
        return latitude;
    }

    public void setLatitude(double latitude) {
        this.latitude = latitude;
    }
}

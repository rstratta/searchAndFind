package uy.com.searchandfind.query;

/**
 * Created by strat on 24/10/2017.
 */

public class SalerAvailableForTenderDTO {
    private double Latitude ;
    private double Length ;
    private String ShopName ;
    private String ShopAddress ;
    private String ShopPhone ;
    private double Distance ;

    public double getLatitude() {
        return Latitude;
    }

    public void setLatitude(double latitude) {
        Latitude = latitude;
    }

    public double getLength() {
        return Length;
    }

    public void setLength(double length) {
        Length = length;
    }

    public String getShopName() {
        return ShopName;
    }

    public void setShopName(String shopName) {
        ShopName = shopName;
    }

    public String getShopAddress() {
        return ShopAddress;
    }

    public void setShopAddress(String shopAddress) {
        ShopAddress = shopAddress;
    }

    public String getShopPhone() {
        return ShopPhone;
    }

    public void setShopPhone(String shopPhone) {
        ShopPhone = shopPhone;
    }

    public double getDistance() {
        return Distance;
    }

    public void setDistance(double distance) {
        Distance = distance;
    }
}

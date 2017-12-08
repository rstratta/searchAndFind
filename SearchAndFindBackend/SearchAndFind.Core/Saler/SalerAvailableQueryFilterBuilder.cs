using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    public class SalerAvailableQueryFilterBuilder
    {
        private readonly static string DISTANCE_PROPERTY_NAME = "distance";
        private readonly static string EARTH_RADIUS_PROPERTY_NAME = "earthRadius";
        private readonly static string NORTH = "north";
        private readonly static string SOUTH = "south";
        private readonly static string EAST = "east";
        private readonly static string WEST = "west";

        public static SalerAvailablesToTenderQueryFilter BuildFilter(double latitude, double length, DateTime queryDate, Guid categoryId)
        {
            double distance = GetPropertyFromConfiguration(DISTANCE_PROPERTY_NAME);
            double earthRadius = GetPropertyFromConfiguration(EARTH_RADIUS_PROPERTY_NAME);
            SalerAvailablesToTenderQueryFilter filter = BuildCardinalQueryFilter(latitude, length, distance, earthRadius);
            FillOtherFilters(filter, categoryId, queryDate);
            return filter;
        }
       

        private static void FillOtherFilters(SalerAvailablesToTenderQueryFilter filter, Guid categoryId,DateTime queryDate)
        {
            filter.CategoryId = categoryId;
            filter.HourQuery = queryDate.Hour;
            filter.DayOfQuery = GetDayOfQuery(queryDate.DayOfWeek);
        }

        private static SalerAvailablesToTenderQueryFilter BuildCardinalQueryFilter(double latitude, double length, double distance, double earthRadius)
        {
            double latitudeInRadian = Degree2Rad(latitude);
            double lengthInRadian = Degree2Rad(length);
            double angleInRadian = distance / earthRadius;
            SalerAvailablesToTenderQueryFilter filter = BuildSalerAvailablesQueryWithCardinalCoords(latitudeInRadian, lengthInRadian, angleInRadian);
            filter.QueryLatitude = latitude;
            filter.QueryLength = length;
            filter.EarthRadius = earthRadius;
            filter.Distance = distance;
            return filter;

        }

        private static SalerAvailablesToTenderQueryFilter BuildSalerAvailablesQueryWithCardinalCoords(double latitudeInRadian, double lengthInRadian, double angleInRadian)
        {
            Dictionary<string, CardinalCoords> cardinalsCoords = BuildCardinalsCoords();
            foreach (var cardinalCoord in cardinalsCoords)
            {
                double cardinalAngleInRadian = Degree2Rad(cardinalCoord.Value.CardinalAngle);
                cardinalCoord.Value.LatitudeResult = Math.Asin(Math.Sin(latitudeInRadian) * Math.Cos(angleInRadian) + Math.Cos(latitudeInRadian) * Math.Sin(angleInRadian) * Math.Cos(cardinalAngleInRadian));
                cardinalCoord.Value.LenghtResult = lengthInRadian + Math.Atan2(Math.Sin(angleInRadian) * Math.Sin(cardinalAngleInRadian) * Math.Cos(latitudeInRadian), Math.Cos(angleInRadian) - Math.Sin(latitudeInRadian) * Math.Sin(cardinalCoord.Value.LatitudeResult));
            }
            SalerAvailablesToTenderQueryFilter filter = new SalerAvailablesToTenderQueryFilter();
            filter.MaxLatitude = RadianToDegree(cardinalsCoords[NORTH].LatitudeResult);
            filter.MinLatitude = RadianToDegree(cardinalsCoords[SOUTH].LatitudeResult);
            filter.MaxLength = RadianToDegree(cardinalsCoords[EAST].LenghtResult);
            filter.MinLength = RadianToDegree(cardinalsCoords[WEST].LenghtResult);
            return filter;
        }

       

        private static double Degree2Rad(double value)
        {
            return Math.PI * value / 180.0;
        }

        private static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        private static double GetPropertyFromConfiguration(string propertyName)
        {
            return double.Parse(ConfigurationManager.AppSettings.Get(propertyName));
        }
        private static Dictionary<string, CardinalCoords> BuildCardinalsCoords()
        {
            Dictionary<string, CardinalCoords> cardinalCoords = new Dictionary<string, CardinalCoords>();
            cardinalCoords.Add(NORTH, new CardinalCoords() { CardinalName = NORTH, CardinalAngle = 0 });
            cardinalCoords.Add(SOUTH, new CardinalCoords() { CardinalName = SOUTH, CardinalAngle = 180 });
            cardinalCoords.Add(EAST, new CardinalCoords() { CardinalName = EAST, CardinalAngle = 90 });
            cardinalCoords.Add(WEST, new CardinalCoords() { CardinalName = WEST, CardinalAngle = 270 });
            return cardinalCoords;
        }

        private static string GetDayOfQuery(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "D";
                case DayOfWeek.Monday:
                    return "L";
                case DayOfWeek.Tuesday:
                    return "M";
                case DayOfWeek.Wednesday:
                    return "X";
                case DayOfWeek.Thursday:
                    return "J";
                case DayOfWeek.Friday:
                    return "V";
                case DayOfWeek.Saturday:
                    return "S";
                default:
                    return "error";
            }
        }

        internal class CardinalCoords
        {
            public string CardinalName { get; set; }
            public int CardinalAngle { get; set; }
            public double LatitudeResult { get; set; }
            public double LenghtResult { get; set; }
        }
    }
}

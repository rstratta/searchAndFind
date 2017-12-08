# Investigación #

## Notificaciones a celulares ##

### Uso que le vamos a dar ###

Para realizar las notificaciones a dispositivos utilizaremos GCM.
Las notificaciones ocurren cuándo:

** El cliente realiza una búsqueda de un producto, y el servidor envía a cloud que se notifique a determinados dispositivos.

** El cliente envía mensaje al vendedor, lo recibe el servidor y este envia a cloud para que notifique al vendedor.

** El vendedor responde a la pregunta del cliente enviando el mensaje al servidor y este a cloud para que notifique el vendedor.

### Limitantes ###

Tamaño máximo del mensaje 4Kb

### Leer mensaje en celular ###

Código para recibir mensaje desde clooud en Android.

```
@Override
public void onMessageReceived(String from, Bundle data) {
   String message = data.getString("message");
   Log.d(TAG, "From: " + from);
   Log.d(TAG, "Message: " + message);
  	// Handle received message here.
 }
```
### Tipos de mensaje ###

#### Mensajes de notificación ####

```
{
    "to" : "bk3RNwTe3H0:CI2k_HHwgIpoDKCIZvvDMExUdFQ3P1...",
    "notification" : {
      "body" : "great match!",
      "title" : "Portugal vs. Denmark",
      "icon" : "myicon"
    }
}
```

#### Mensajes de datos ####

```
{
    "to" : "bk3RNwTe3H0:CI2k_HHwgIpoDKCIZvvDMExUdFQ3P1...",
    "data" : {
      "body" : "great match!",
      "title" : "Portugal vs. Denmark",
      "icon" : "myicon"
    }
}
```


### Envío de mensaje a dispositivos ###

#### Envío a cloud ####

Solicitud POST

```
https://fcm.googleapis.com/fcm/send
Content-Type:application/json
Authorization:key=AIzaSyZ-1u...0GBYzPu7Udno5aA


{ "data": {
    "score": "5x1",
    "time": "15:10"
  },
  "to" : "bk3RNwTe3H0:CI2k_HHwgIpoDKCIZvvDMExUdFQ3P1..."
}
```

#### Para enviar a grupo de celulares ####

Por lo que entiendo, el grupo de celulares se puede crear en el cloud. Esto creo que no nos sirve

#### Enviar por topics ####

Preguntar en clase, si sirve que los locales se registren a una categoría

```
https://fcm.googleapis.com/fcm/send
Content-Type:application/json
Authorization:key=AIzaSyZ-1u...0GBYzPu7Udno5aA
{
  "to": "/topics/foo-bar",
  "data": {
    "message": "This is a Firebase Cloud Messaging Topic Message!",
   }
}
```


### Prioridad de los mensajes ###

Prioridad de los mensajes: hay dos opciones, crítico o normal. En este caso usaremos normal para todo tipo de mensaje a notificar.



## Google Maps ##

### Clave para la API ###

Key: AIzaSyDQDLeLUZIvRHZ8wwt1MndPWsmaD6v46BM

### build.gradle ###

Agregar 'com.google.android.gms:play-services-location:7.0.0'

### Obtener location 

Para obtener la location del cliente al momento de hacer la consulta:

#### Código ####

```
if (mGoogleApiClient == null) {
    mGoogleApiClient = new GoogleApiClient.Builder(this)
        .addConnectionCallbacks(this)
        .addOnConnectionFailedListener(this)
        .addApi(LocationServices.API)
        .build();
}
if (mGoogleApiClient != null) {
    mGoogleApiClient.connect();
}

public class MainActivity extends ActionBarActivity implements
        ConnectionCallbacks, OnConnectionFailedListener {
    ...
    @Override
    public void onConnected(Bundle connectionHint) {
        mLastLocation = LocationServices.FusedLocationApi.getLastLocation(
                mGoogleApiClient);
        if (mLastLocation != null) {
            mLatitudeText.setText(String.valueOf(mLastLocation.getLatitude()));
            mLongitudeText.setText(String.valueOf(mLastLocation.getLongitude()));
        }
    }
}

```

#### Agregar marcadores en el mapa ####

```
@Override
public void onMapReady(GoogleMap map) {
    map.addMarker(new MarkerOptions()
        .position(new LatLng(10, 10))
        .title("Hello world"));
}

```

La documentación de Google está re completa para mostrar información del marcador. Ejemplo podríamos agregar el nombre del local y el precio del producto que se busca.

## Algoritmo para búsqueda de puntos cercanos ##

Lo que pude investigar es que para este algoritmo, una forma de calcular es con el teorema de coseno.

Para estos cálculos, se necesita en el radio de la tierra 6371 KM, la longitud y latitud desde el punto que se buscan los lugares más cercanos.

El ejemplo, se deriva la responsabilidad del cálculo a la BD, la estructura de datos que utilizan, coincide plenamente con la que definimos inicialmente.

Para optimizar la búsqueda, se puede realizar un cálculo de área donde los límites son los metros que se desean buscar a la redonda. 

El ejemplo que se muestra es en PHP

```
function getBoundaries($lat, $lng, $distance = 1, $earthRadius = 6371)
{
    $return = array();
     
    // Los angulos para cada dirección
    $cardinalCoords = array('north' => '0',
                            'south' => '180',
                            'east' => '90',
                            'west' => '270');
    $rLat = deg2rad($lat);
    $rLng = deg2rad($lng);
    $rAngDist = $distance/$earthRadius;
    foreach ($cardinalCoords as $name => $angle)
    {
        $rAngle = deg2rad($angle);
        $rLatB = asin(sin($rLat) * cos($rAngDist) + cos($rLat) * sin($rAngDist) * cos($rAngle));
        $rLonB = $rLng + atan2(sin($rAngle) * sin($rAngDist) * cos($rLat), cos($rAngDist) - sin($rLat) * sin($rLatB));
         $return[$name] = array('lat' => (float) rad2deg($rLatB), 
                                'lng' => (float) rad2deg($rLonB));
    }
    return array('min_lat'  => $return['south']['lat'],
                 'max_lat' => $return['north']['lat'],
                 'min_lng' => $return['west']['lng'],
                 'max_lng' => $return['east']['lng']);
}

```

Finalmente, ya con la latitud y longitud acotada, podemos realizar la siguiente consulta:

```
'SELECT *, (6371 * ACOS( 
                                            SIN(RADIANS(lat)) 
                                            * SIN(RADIANS(' . $lat . ')) 
                                            + COS(RADIANS(lng - ' . $lng . ')) 
                                            * COS(RADIANS(lat)) 
                                            * COS(RADIANS(' . $lat . '))
                                            )
                               ) AS distance
                     FROM direcciones
                     WHERE (lat BETWEEN ' . $box['min_lat']. ' AND ' . $box['max_lat'] . ')
                     AND (lng BETWEEN ' . $box['min_lng']. ' AND ' . $box['max_lng']. ')
                     HAVING distance  < ' . $distance . '                                       
                     ORDER BY distance ASC '
```                 
# Car Advert Case

This application contains a basic car advert list and detail endpoints. The app is using .Net Core, ElasticSearch, RabbitMQ & SqlServer and running on docker.

You can run the application with the following command;
```
docker compose up
```
	
ElasticSearch use default port 9200.

RabbitMQ use default port 15672.

SqlServer use default port 14333.

If you want, you can change from <b>docker-compose-override.yml</b>.

## First you must create default data. Run the following endpoint; 
```
/api/data/seed
```

The enums for filter;

## Gear
| Code | Reason |
| ---- | ------ |
| 1  | Manual |
| 2  | Auto |
| 3  | Semi-Auto |

## Fuel
| Code | Reason |
| ---- | ------ |
| 1  | Gasoline |
| 2  | FuelOil |
| 3  | Gas & Gasoline |

## Sort Type
| Code | Reason |
| ---- | ------ |
| 1  | Price Asc |
| 2  | Price Desc |
| 3  | Km Asc |
| 4  | Km Desc |
| 5  | Year Asc |
| 6  | Year Desc |


Run the following endpoint for advert list;
```
/api/advert/all
```
 
Filtering Fields:  categoryId, price, gear, fuel, page
Sorting Fields: price, year, km

Run the following endpoint for advert detail;
```
/api/advert/get/{id}
```

Run the following endpoint for advert visit record; 
```
/api/advert/visit
```

Sample Request Body: 
```
{
  advertId: "15763767"
}
```

For try api and for details;
```
/swagger/index.html
```
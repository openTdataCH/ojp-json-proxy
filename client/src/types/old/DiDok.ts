export interface DiDok {
    "total_count": number,
    "results": DiDokResult[],
}

export interface DiDokResult {
    "lod": string,
    "geopos": {
        "lon": number,
        "lat": number
    },
    "numbershort": number,
    "uiccountrycode": number,
    "sloid": string,
    "number": number,
    "validfrom": Date,
    "validto": Date,
    "designationofficial": string,
    "designationlong": string | null,
    "abbreviation": string,
    "operatingpoint": boolean,
    "operatingpointwithtimetable": boolean,
    "stoppoint": boolean,
    "stoppointtype": string,
    "freightservicepoint": boolean,
    "trafficpoint": boolean,
    "borderpoint": boolean,
    "hasgeolocation": boolean,
    "isocountrycode": string,
    "cantonname": string,
    "cantonfsonumber": string,
    "cantonabbreviation": string,
    "districtname": string,
    "districtfsonumber": string,
    "municipalityname": string,
    "fsonumber": string,
    "localityname": string,
    "operatingpointtype": string | null,
    "operatingpointtechnicaltimetabletype": string | null,
    "meansoftransport": string,
    "categories": string | null,
    "operatingpointtrafficpointtype": string | null,
    "operatingpointroutenetwork": boolean,
    "operatingpointkilometer": boolean,
    "operatingpointkilometermasternumber": string,
    "sortcodeofdestinationstation": string,
    "businessorganisation": string,
    "businessorganisationnumber": number,
    "businessorganisationabbreviationde": string,
    "businessorganisationabbreviationfr": string,
    "businessorganisationdescriptionde": string,
    "businessorganisationdescriptionfr": string,
    "fotcomment": string | null,
    "height": number,
    "creationdate": Date,
    "editiondate": Date,
}
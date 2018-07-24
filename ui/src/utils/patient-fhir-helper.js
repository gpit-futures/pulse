export function convertToFhir(patient) {
    let fhirPatient = 
    {
        "resourceType": "Patient",
        "id": patient.id,
        "identifier": [
          {
            "system": "https://fhir.nhs.uk/Id/nhs-number",
            "value": patient.nhsNumber
          }
        ],
        "active": true,
        "name": [
          {
            "use": "usual",
            "family": patient.lastName,
            "given": [
              patient.firstName
            ],
            "prefix": [
              patient.title
            ]
          }
        ],
        "telecom": [
          {
            "system": "phone",
            "value": patient.phone,
            "use": "home"
          }
        ],
        "gender": patient.gender.toLowerCase(),
        "birthDate": patient.dateOfBirth,
        "address": [
          {
            "use": "work",
            "type": "both",
            "line": [
              patient.address.line1,
              patient.address.line2
            ],
            "city": patient.address.line3,
            "district": "test",
            "postalCode": patient.address.postcode
          }
        ],
        "gp": patient.gp
      }
    return fhirPatient
}
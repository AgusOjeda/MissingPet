import React, { useState, useEffect } from "react";
import api from "../api/pets";
import DataTable from "react-data-table-component";
const columnas = [
  {
    name: "Id",
    selector: "id",
    sortable: true,
  },
  {
    name: "Species",
    selector: "species",
    sortable: true,
  },
  {
    name: "Name",
    selector: "name",
    sortable: true,
  },
  {
    name: "Breed",
    selector: "breed",
    sortable: true,
  },
  {
    name: "Size",
    selector: "size",
    sortable: true,
  },
  {
    name: "Sex",
    selector: "sex",
    sortable: true,
  },
  {
    name: "Color",
    selector: "color",
    sortable: true,
  },
  {
    name: "Plate",
    selector: "plate",
    sortable: true,
  },
  {
    name: "Observations",
    selector: "observations",
    sortable: true,
  },
  {
    name: "Location",
    selector: "location",
    sortable: true,
  },
  {
    name: "Pictures",
    selector: "pictures",
    sortable: true,
  },
  {
    name: "Created Date",
    selector: "createdDate",
    sortable: true,
  },
];
function Table() {
  const [pets, setPets] = useState([]);

  const retrievePets = async () => {
    const response = await api.get();
    return response;
  };
  useEffect(() => {
    const getAllPets = async () => {
      const allPets = await retrievePets();
      console.log(allPets);
      if (allPets) setPets(allPets.data);
    };
    getAllPets();
  }, []);

  return (
    <div className="table-responsive">
      <DataTable columns={columnas} data={pets} title="Pets" pagination />
    </div>
  );
}
export default Table;

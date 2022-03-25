import React, { useState, useEffect } from "react";
import {
  Table,
  Thead,
  Tbody,
  Tfoot,
  Tr,
  Th,
  Td,
  TableCaption,
  Text,
  Box,
} from "@chakra-ui/react";
import axios from "axios";
import api from "../api/pets";
function Read() {
  const [pets, setPets] = useState([]);

  const retrievePets = async () => {
    const response = await api.get();
    return response;
  };
  /*
  useEffect(() => {
    const get = () => {
      const response = axios
        .get("https://localhost:5001/pets")
        .then((response) => setPets(response.data))
        .catch((reason) => console.log(reason.message));
    };
    get();
  }, []);*/
  useEffect(() => {
    const getAllPets = async () => {
      const allPets = await retrievePets();
      console.log(allPets);
      if (allPets) setPets(allPets.data);
    };
    getAllPets();
  }, []);

  const useaxiosPosts = pets.map((pets) => {
    return (
      <Table size="sm" variant="striped">
        <Thead>
          <Tr>
            <Th>Id</Th>
            <Th>Species</Th>
            <Th>Name</Th>
            <Th>Breed</Th>
            <Th>Size</Th>
            <Th>Sex</Th>
            <Th>Color</Th>
            <Th>Plate</Th>
            <Th>Observations</Th>
            <Th>Location</Th>
            <Th>Pictures</Th>
            <Th>Created Date</Th>
          </Tr>
        </Thead>

        <Tbody>
          <Tr key={pets.id}>
            <Td>{pets.id}</Td>
            <Td>{pets.species}</Td>
            <Td>{pets.name}</Td>
            <Td>{pets.breed}</Td>
            <Td>{pets.size}</Td>
            <Td>{pets.sex}</Td>
            <Td>{pets.color}</Td>
            <Td>{pets.plate}</Td>
            <Td>{pets.observations}</Td>
            <Td>{pets.location}</Td>
            <Td>{pets.pictures}</Td>
            <Td>{pets.createdDate}</Td>
          </Tr>
        </Tbody>
      </Table>
    );
  });

  return (
    <>
      <Box margin={2} borderRadius={9} borderColor="black" border={"1px"}>
        {pets && useaxiosPosts}
      </Box>
    </>
  );
}

export default Read;

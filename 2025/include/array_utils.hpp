#include <array>
#include <cstddef>
#include <iostream>

using std::array;

template <typename T, size_t Row, size_t Col>
using Array2 = array<array<T, Col>, Row>;

template <typename T, size_t Row, size_t Col>
void print2DArray(const Array2<T, Row, Col>& arr) {

    for (array<T, Col> row : arr) {
        std::cout << "[";

        for (size_t i = 0; i < row.size() - 1; i++) {
            std::cout << row[i] << ", ";
        }

        std::cout << row[row.size() - 1] << "]" << std::endl;
    }
}
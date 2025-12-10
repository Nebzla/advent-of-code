#include <cstddef>
#include <ostream>
#include <stdexcept>
#include <utility>
#include <vector>
using std::vector;

template<typename T>
class Vector2 {
    
    vector<vector<T>> data;

public:

    Vector2(const T& defaultValue, const size_t& rows, const size_t& cols) {
        if(rows == 0 || cols == 0) throw std::invalid_argument("Insufficient rows or columns provided");

        for(size_t r = 0; r < rows; r++) {
            data.push_back(vector<T>(cols, defaultValue));
        }
    }

    size_t rows() const {
        return data.size();
    }

    size_t cols() const {
        return data[0].size();
    }

    const T& at(const size_t& row, const size_t& col) const {
        return data[row][col];
    }

    void set(const size_t& row, const size_t& col, T value) {
        data[row][col] = value;
    }


    bool isCoordInBounds(const long row, const long col) const {
        return row >= 0 && col >= 0 && row < rows() && col < cols();
    }


    // Only necessary for row as built in to 1D vector
    const vector<T>& operator[](const size_t& row) const { 
        return data[row];
    }

    std::pair<size_t, size_t> positionOf(const T& query) const {
        for(size_t r = 0; r < rows(); r++) {
            for(size_t c = 0; c < cols(); c++) {
                if(data[r][c] == query) return std::make_pair(r, c);
            }
        }

        return std::make_pair(-1, -1);
    }
};

template<typename T>
std::ostream& operator<<(std::ostream& os, const Vector2<T>& vect) {
    for (size_t r = 0; r < vect.rows(); r++) {
        
        os << '[';

        for (size_t c = 0; c < vect.cols() - 1; c++) {
            os << vect[r][c] << ", ";
        }

        os << vect[r][vect.cols() - 1] << "]\n";
    }

    return os;
}
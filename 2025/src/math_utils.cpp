#include "math_utils.hpp"
#include <cmath>


int getDigits(const int& num) {
    return std::log10(num) + 1;
}
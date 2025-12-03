#include "day.hpp"
#include <iostream>

void Day::outputSolutions(const std::string &input) {
    std::cout << "Part A: " << solvePartA(input) << std::endl;
    std::cout << "Part B: " << solvePartB(input) << std::endl;
}
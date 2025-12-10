#pragma once
#include "day.hpp"
#include "vector_utils.hpp"

class Day4 : public Day {
public:
    string solvePartA(const string& input) override;
    string solvePartB(const string& input) override;
private:
    Vector2<bool> getFormattedGrid(const string& input);
    bool isAccessible(const Vector2<bool>& grid, const size_t& row, const size_t& col);
};
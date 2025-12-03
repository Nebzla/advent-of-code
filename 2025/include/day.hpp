#pragma once
#include <string>

class Day {
public:
  virtual ~Day() = default;
  virtual std::string solvePartA(const std::string &input) = 0;
  virtual std::string solvePartB(const std::string &input) = 0;

  void outputSolutions(const std::string &input);
};
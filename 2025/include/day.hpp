#pragma once
#include <string>
#include <vector>
#include "benchmark.hpp"

using std::string, std::vector;

using ResultStr = BenchmarkResult<string>;
void printResults(const string& identifier, const ResultStr result);

class Day {
public:
  virtual ~Day() = default;
  void solve(const string &input);
private:
  virtual string solvePartA(const string &input) = 0;
  virtual string solvePartB(const string &input) = 0;
};
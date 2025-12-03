#include "string_utils.hpp"
#include <sstream>
#include <string>

vector<string> splitStringByLines(const string &text) {
    vector<string> lines;

    std::stringstream ss(text);
    string line;

    while(std::getline(ss, line)) {
        if(!line.empty()) lines.push_back(line);
    }

    return lines;
}
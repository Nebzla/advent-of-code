#include "string_utils.hpp"
#include <sstream>
#include <string>
#include <vector>

using std::string, std::vector;

vector<string> splitStringByLines(const string& text) {
    vector<string> lines;

    std::stringstream ss(text);
    string line;

    while(std::getline(ss, line)) {
        if(!line.empty()) lines.push_back(line);
    }

    return lines;
}

vector<string> splitStringByDelimiter(const string& text, const char& delimiter) {
    vector<string> substrings;

    std::stringstream ss(text);
    string substr;
    
    while(getline(ss, substr, delimiter)) {
        substrings.push_back(substr);
    }

    return substrings;
}
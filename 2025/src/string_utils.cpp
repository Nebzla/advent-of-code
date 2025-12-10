#include "string_utils.hpp"
#include <sstream>
#include <string>
#include <utility>


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

std::pair<string, string> splitStringByFirstOccurence(const string& text, const string delimiter) {
    size_t splitPoint = text.find(delimiter);
    if(splitPoint == string::npos) return std::make_pair("", "");

    string first = text.substr(0, splitPoint);
    string second = text.substr(splitPoint + delimiter.length());

    return std::make_pair(first, second);
}


std::pair<long, long> parseRange(const string& range) {
    vector<string> values = splitStringByDelimiter(range, '-');
    if(values.size() != 2) throw std::length_error("Range was parsed incorrectly");

    return std::make_pair(stol(values[0]), stol(values[1]));
}



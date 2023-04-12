TARGET_FILE=$1
echo $TARGET_FILE

TARGET_NAME="$(echo $TARGET_FILE | sed 's/\.c//')"
echo $TARGET_NAME

gcc -fPIC -c -o $TARGET_NAME".o" $TARGET_FILE
gcc -shared -o $TARGET_NAME".so" $TARGET_NAME".o" -ldl

export function formatDate(value) {
    if (!value) return '';
    return new Date(value).toLocaleDateString('da-DK', {
        day: 'numeric',
        month: 'long',
        year: 'numeric'
    })
}
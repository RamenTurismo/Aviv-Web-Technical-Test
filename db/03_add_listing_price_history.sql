CREATE TABLE IF NOT EXISTS public.listing_price_history
(
    id                   serial           primary key,
    listing_id           INT,
    price                double precision not null,
    created_at           timestamp,
    CONSTRAINT fk_listing_id FOREIGN KEY(listing_id) REFERENCES listing(id)
);